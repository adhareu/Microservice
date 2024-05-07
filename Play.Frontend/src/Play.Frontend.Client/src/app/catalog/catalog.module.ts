import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CatalogRoutingModule } from './catalog-routing.module';
import { CatalogComponent } from './catalog.component';
import { CatalogAddComponent } from './catalog-add/catalog-add.component';
import { CatalogEditComponent } from './catalog-edit/catalog-edit.component';


@NgModule({
  declarations: [
    CatalogComponent,
    CatalogAddComponent,
    CatalogEditComponent
  ],
  imports: [
    CommonModule,
    CatalogRoutingModule
  ]
})
export class CatalogModule { }
