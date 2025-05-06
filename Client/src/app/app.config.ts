import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { AppComponent } from './app.component';

export const appConfig = {
  providers: [
    provideHttpClient(),
    provideRouter(routes),
  ],
};
