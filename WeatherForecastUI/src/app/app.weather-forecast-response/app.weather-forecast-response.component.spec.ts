import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppWeatherForecastResponseComponent } from './app.weather-forecast-response.component';

describe('AppWeatherForecastResponseComponent', () => {
  let component: AppWeatherForecastResponseComponent;
  let fixture: ComponentFixture<AppWeatherForecastResponseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AppWeatherForecastResponseComponent]
    });
    fixture = TestBed.createComponent(AppWeatherForecastResponseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
