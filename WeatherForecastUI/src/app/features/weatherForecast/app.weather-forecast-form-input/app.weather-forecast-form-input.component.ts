import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AppWeatherForecastService } from '../../../core/services/app.weather-forecast-service';
import { Observable } from 'rxjs';
import { LoadingState } from '../../../core/models/loading-state.model';

@Component({
  selector: 'app-weather-forecast-form-input',
  templateUrl: './app.weather-forecast-form-input.component.html'
})
export class AppWeatherForecastFormInputComponent implements OnInit {
  shouldIncludeTodayValues: string[] = ['Yes', 'No']
  weatherForecastForm!: FormGroup;
  weatherForecastData: any
  errors: string[] = [];
  observableData: Observable<any> | null = null;
  loading = LoadingState.NOT_LOADED;
  LoadingState = LoadingState;

  constructor(private readonly formBuilder: FormBuilder,private readonly appWeatherForecastService: AppWeatherForecastService) { }
    
  ngOnInit(): void {
     this.createForm();
  }

  createForm() {
    this.weatherForecastForm = this.formBuilder.group(
      {
        city: ['', Validators.required],
        numberOfDaysToForecast: ['', Validators.required],
        shouldIncludeToday: [''],
      }
    )
  }

  onSubmit() {
    this.loading = LoadingState.LOADING;
    this.weatherForecastData = this.weatherForecastForm.value;
    console.log("weatherForecastData : " + this.weatherForecastData);
    if (this.weatherForecastForm.valid) {
      this.errors = [];
      this.observableData = this.appWeatherForecastService.fetchWeatherForecastData(this.weatherForecastData.city, this.weatherForecastData.numberOfDaysToForecast, this.weatherForecastData.shouldIncludeToday = 'No' ? 'false' : 'true');
      this.observableData.subscribe({
        next: (responseData) => this.appWeatherForecastService.subscribe(responseData),
        error: (err) => {
          this.errors.push(err);
          this.appWeatherForecastService.subscribe(null);
          this.loading = LoadingState.LOADED
        },
        complete: () => this.loading = LoadingState.LOADED
      })
    }
  }
}
