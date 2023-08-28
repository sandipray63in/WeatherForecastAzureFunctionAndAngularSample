import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppWeatherForecastFormInputComponent } from './app.weather-forecast-form-input/app.weather-forecast-form-input.component';
import { AppWeatherForecastResponseComponent } from './app.weather-forecast-response/app.weather-forecast-response.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptorService } from './services/interceptors/auth-interceptor.service';
import { LogInterceptorService } from './services/interceptors/log-interceptor.service';
import { AppWeatherForecastService } from './services/app.weather-forecast-service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AgGridModule } from 'ag-grid-angular';

@NgModule({
  declarations: [
    AppComponent,
    AppWeatherForecastFormInputComponent,
    AppWeatherForecastResponseComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    AgGridModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LogInterceptorService,
      multi: true
    },
    AppWeatherForecastService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
