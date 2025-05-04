import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ApiConfigService} from './api-config.service';
import {Ship} from '../models/ship';

@Injectable({
  providedIn: 'root'
})
export class ShipService {
  private readonly endpoint: string;

  constructor(private http: HttpClient, private api: ApiConfigService) {
    this.endpoint = `${this.api.baseUrl}/ships`;
  }

  getAll(): Observable<Ship[]> {
    return this.http.get<Ship[]>(this.endpoint);
  }

  get(id: number): Observable<Ship> {
    return this.http.get<Ship>(`${this.endpoint}/${id}`);
  }

  create(ship: Ship): Observable<Ship> {
    return this.http.post<Ship>(this.endpoint, ship);
  }

  update(id: number, ship: Ship): Observable<void> {
    return this.http.put<void>(`${this.endpoint}/${id}`, ship);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.endpoint}/${id}`);
  }
}
