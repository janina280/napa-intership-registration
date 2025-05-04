import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config'; //todo: this was deleted
import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
