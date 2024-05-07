using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Play.Common;
using Play.Inventory.Service.Clients;
using Play.Inventory.Service.Entities;
using static Play.Inventory.Service.Dtos.Dtos;

namespace Play.Inventory.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IRepository<InventoryItem> inventoryItemsRepository;
        private readonly IRepository<CatalogItem> catalogItemsRepository;
        public InventoryController(IRepository<InventoryItem> inventoryItemsRepository, IRepository<CatalogItem> catalogItemsRepository)
        {
            this.inventoryItemsRepository = inventoryItemsRepository;
            this.catalogItemsRepository = catalogItemsRepository;
            
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync()
        {
            

            var inventoryItemEntities = await inventoryItemsRepository.GetAllAsync();
            var itemIds = inventoryItemEntities.Select(item => item.CatalogItemId);
            var catelogItems = await catalogItemsRepository.GetAllAsync(item => itemIds.Contains(item.Id));
            var inventoryItemDtos = inventoryItemEntities.Select(inventoryItem =>
            {
                var catelogItem = catelogItems.FirstOrDefault(catalogItem => catalogItem.Id == inventoryItem.CatalogItemId);
                return inventoryItem.AsDto(catelogItem.Name, catelogItem.Description);
            });

            return Ok(inventoryItemDtos);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(Guid userId)
        {
            if(userId==Guid.Empty)
            {
                return BadRequest();
            }
           
            var inventoryItemEntities = await inventoryItemsRepository.GetAllAsync(item => item.UserId == userId);
            var itemIds = inventoryItemEntities.Select(item => item.CatalogItemId);
            var catelogItems = await catalogItemsRepository.GetAllAsync(item=>itemIds.Contains(item.Id));
            var inventoryItemDtos = inventoryItemEntities.Select(inventoryItem =>
            {
                var catelogItem = catelogItems.FirstOrDefault(catalogItem => catalogItem.Id == inventoryItem.CatalogItemId);
                return inventoryItem.AsDto(catelogItem.Name, catelogItem.Description);
            });

            return Ok(inventoryItemDtos);
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(GrandItemDto grandItemDto)
        {
            var inventoryItem = await inventoryItemsRepository.GetAsync(x => x.UserId == grandItemDto.UserId && 
            x.CatalogItemId==grandItemDto.CatalogItemId);
            if(inventoryItem==null)
            {
                inventoryItem = new InventoryItem
                {
                    CatalogItemId=grandItemDto.CatalogItemId,
                    UserId=grandItemDto.UserId,
                    Quantity=grandItemDto.Quantity,
                    AccquiredDate=DateTimeOffset.UtcNow
                };
                await inventoryItemsRepository.CreateAsync(inventoryItem);
            }
            else
            {
                inventoryItem.Quantity += grandItemDto.Quantity;
                await inventoryItemsRepository.UpdateAsync(inventoryItem);
            }
            return Ok();
        }
    }
}
