import { Component, OnInit } from '@angular/core';
import { HttpService } from '../core/services/http.service';
import { environment } from '../../environments/environment.development';
@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrl: './inventory.component.css',
})
export class InventoryComponent implements OnInit {
  inventoryListItems = [];
  constructor(public httpService: HttpService) {}
  ngOnInit(): void {
    this.loadData();
  }
  loadData() {
    this.httpService
      .get(environment.Base_URL_Inventory, 'inventory')
      .subscribe({
        next: (response: any) => {
          this.inventoryListItems = response;
        },
        error: (response) => {
          console.log(response);
        },
      });
  }
}
