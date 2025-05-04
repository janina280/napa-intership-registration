import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ApiConfigService} from './api-config.service';
import {Voyage} from '../models/voyage';

@Injectable({
  providedIn: 'root'
})
export class VoyageService {
  private readonly endpoint: string;

  constructor(private http: HttpClient, private api: ApiConfigService) {
    this.endpoint = `${this.api.baseUrl}/voyages`;
  }

  getAll(): Observable<Voyage[]> {
    return this.http.get<Voyage[]>(this.endpoint);
  }

  get(id: number): Observable<Voyage> {
    return this.http.get<Voyage>(`${this.endpoint}/${id}`);
  }

  create(voyage: Voyage): Observable<Voyage> {
    return this.http.post<Voyage>(this.endpoint, voyage);
  }

  update(id: number, voyage: Voyage): Observable<void> {
    return this.http.put<void>(`${this.endpoint}/${id}`, voyage);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.endpoint}/${id}`);
  }
}
