import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InventoryComponent } from './inventory.component';
import { InventoryAddComponent } from './inventory-add/inventory-add.component';
import { InventoryEditComponent } from './inventory-edit/inventory-edit.component';

const routes: Routes = [
  {
    path: 'list',
    component: InventoryComponent,
    data: {
      title: 'Inventory List',
      reuse: true,
    },
  },
  {
    path: 'add',
    //canActivate: [AfterAuthGlobalGuard],
    component: InventoryAddComponent,
    data: {
      title: 'Add Inventory',
      reuse: true,
    },
  },
  {
    path: 'edit/:id',
    //canActivate: [AfterAuthGlobalGuard],
    component: InventoryEditComponent,
    data: {
      title: 'Update Inventory',
      reuse: true,
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InventoryRoutingModule {}
