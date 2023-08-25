import { TestBed } from '@angular/core/testing';

import { AppWeatherForecastService } from './app.weather-forecast-service';

describe('AppWeatherForecastService', () => {
  let service: AppWeatherForecastService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppWeatherForecastService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
