import { CatalogEditComponent } from './catalog-edit/catalog-edit.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CatalogComponent } from './catalog.component';
import { CatalogAddComponent } from './catalog-add/catalog-add.component';

const routes: Routes = [
  {
    path: 'list',
    component: CatalogComponent,
    data: {
      title: 'Catalog List',
      reuse: true,
    },
  },
  {
    path: 'add',
    //canActivate: [AfterAuthGlobalGuard],
    component: CatalogAddComponent,
    data: {
      title: 'Add Catalog',
      reuse: true,
    },
  },
  {
    path: 'edit/:id',
    //canActivate: [AfterAuthGlobalGuard],
    component: CatalogEditComponent,
    data: {
      title: 'Update Catalog',
      reuse: true,
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CatalogRoutingModule {}
