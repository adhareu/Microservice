import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  Observable,
  pipe,
  throwError,
  isObservable,
  firstValueFrom,
} from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
@Injectable({
  providedIn: 'root',
})
export class HttpService {
  constructor(private httpClient: HttpClient) {}
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  get<T>(url: string, endpoint: string): Observable<T> {
    return this.httpClient
      .get<T>(`${url}/${endpoint}`)
      .pipe(retry(0), catchError(this.handleError));
  }

  post<T>(url: string, endpoint: string, item: any): Observable<T> {
    return this.httpClient
      .post<T>(`${url}/${endpoint}`, JSON.stringify(item), this.httpOptions)
      .pipe(retry(0), catchError(this.handleError));
  }
  put<T>(url: string, endpoint: string, item: T, id: any): Observable<T> {
    return this.httpClient
      .put<T>(`${url}/${endpoint}`, JSON.stringify(item), this.httpOptions)
      .pipe(retry(0), catchError(this.handleError));
  }
  delete<T>(url: string, endpoint: string, item: T) {
    return this.httpClient
      .delete<T>(`${url}/${endpoint}`, this.httpOptions)
      .pipe(retry(0), catchError(this.handleError));
  }
  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';

    //error client
    errorMessage = error.error ? error.error[0]?.message : 'Some error occured';

    return throwError(errorMessage ?? 'Something went wrong');
  }
}
