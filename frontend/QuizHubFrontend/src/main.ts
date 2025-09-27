import { bootstrapApplication } from '@angular/platform-browser';
import { importProvidersFrom, enableProdMode } from '@angular/core';
import { AppRoutingModule } from './app/app-routing.module';
import { App } from './app/app';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { jwtInterceptor } from './app/core/interceptors/jwt.interceptors';
import { environment } from './environments/environment';

if (environment.prodaction) {
  enableProdMode();
}

bootstrapApplication(App, {
  providers: [
    importProvidersFrom(AppRoutingModule),
    provideHttpClient(withInterceptors([jwtInterceptor])),
  ],
}).catch((err) => console.error(err));
