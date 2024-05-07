import { Component, OnInit } from '@angular/core';
import { HttpService } from '../../core/services/http.service';

@Component({
  selector: 'app-catalog-add',
  templateUrl: './catalog-add.component.html',
  styleUrl: './catalog-add.component.css',
})
export class CatalogAddComponent implements OnInit {
  ngOnInit(): void {}
  constructor(public httpService: HttpService) {}
  AddCatalog() {}
}
