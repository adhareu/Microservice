import { Component, OnInit } from '@angular/core';
import { HttpService } from '../core/services/http.service';
import { environment } from '../../environments/environment.development';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrl: './catalog.component.css',
})
export class CatalogComponent implements OnInit {
  catalogListItems = [];
  constructor(public httpService: HttpService) {}
  ngOnInit(): void {
    this.loadData();
  }
  loadData() {
    this.httpService.get(environment.Base_URL_Catalog, 'catalog').subscribe({
      next: (response: any) => {
        console.log(response);

        this.catalogListItems = response;
      },
      error: (response) => {
        console.log(response);
      },
    });
  }
}
