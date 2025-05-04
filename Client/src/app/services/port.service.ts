import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ApiConfigService} from './api-config.service';
import {Port} from '../models/port';

@Injectable({
  providedIn: 'root'
})
export class PortService {
  private readonly endpoint: string;

  constructor(private http: HttpClient, private api: ApiConfigService) {
    this.endpoint = `${this.api.baseUrl}/ports`;
  }

  getAll(): Observable<Port[]> {
    return this.http.get<Port[]>(this.endpoint);
  }

  get(id: number): Observable<Port> {
    return this.http.get<Port>(`${this.endpoint}/${id}`);
  }

  create(port: Port): Observable<Port> {
    return this.http.post<Port>(this.endpoint, port);
  }

  update(id: number, port: Port): Observable<void> {
    return this.http.put<void>(`${this.endpoint}/${id}`, port);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.endpoint}/${id}`);
  }
}
