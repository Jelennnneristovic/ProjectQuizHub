import { bootstrapApplication } from '@angular/platform-browser';
import { importProvidersFrom } from '@angular/core';
import { AppRoutingModule } from './app/app-routing.module';
import { App } from './app/app';
import { provideHttpClient } from '@angular/common/http';


bootstrapApplication(App, {
  providers: [
    importProvidersFrom(AppRoutingModule), 
    provideHttpClient(),                   
  ],
}).catch(err => console.error(err));
