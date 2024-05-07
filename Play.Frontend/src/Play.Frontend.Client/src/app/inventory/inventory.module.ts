import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InventoryRoutingModule } from './inventory-routing.module';
import { InventoryComponent } from './inventory.component';
import { InventoryAddComponent } from './inventory-add/inventory-add.component';
import { InventoryEditComponent } from './inventory-edit/inventory-edit.component';


@NgModule({
  declarations: [
    InventoryComponent,
    InventoryAddComponent,
    InventoryEditComponent
  ],
  imports: [
    CommonModule,
    InventoryRoutingModule
  ]
})
export class InventoryModule { }
