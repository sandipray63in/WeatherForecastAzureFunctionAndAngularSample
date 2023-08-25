import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppWeatherForecastFormInputComponent } from './app.weather-forecast-form-input.component';

describe('AppWeatherForecastFormInputComponent', () => {
  let component: AppWeatherForecastFormInputComponent;
  let fixture: ComponentFixture<AppWeatherForecastFormInputComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AppWeatherForecastFormInputComponent]
    });
    fixture = TestBed.createComponent(AppWeatherForecastFormInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
