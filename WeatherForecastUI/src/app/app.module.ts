import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AgGridModule } from 'ag-grid-angular';
import { AppWeatherForecastFormInputComponent } from './features/weatherForecast/app.weather-forecast-form-input/app.weather-forecast-form-input.component';
import { AppWeatherForecastResponseComponent } from './features/weatherForecast/app.weather-forecast-response/app.weather-forecast-response.component';
import { AuthInterceptorService } from './core/interceptors/auth-interceptor.service';
import { LogInterceptorService } from './core/interceptors/log-interceptor.service';
import { AppWeatherForecastService } from './core/services/app.weather-forecast-service';
import { ApiInterceptorService } from './core/interceptors/api-interceptor.service';
import { ErrorInterceptorService } from './core/interceptors/error-interceptor.service';
import { ListErrorsComponent } from './shared/list-errors.component';

@NgModule({
  declarations: [
    AppComponent,
    AppWeatherForecastFormInputComponent,
    AppWeatherForecastResponseComponent,
    ListErrorsComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    AgGridModule
  ],
  providers: [
    //order of the interceptors is important here.
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ApiInterceptorService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptorService,
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
