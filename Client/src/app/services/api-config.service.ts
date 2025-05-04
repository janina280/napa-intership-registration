import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiConfigService {
  get baseUrl(): string {
    return environment.apiBaseUrl;
  }
}
