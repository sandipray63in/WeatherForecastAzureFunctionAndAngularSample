import { Component, OnInit } from '@angular/core';
import { AppWeatherForecastService } from '../services/app.weather-forecast-service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-weather-forecast-form-input',
  templateUrl: './app.weather-forecast-form-input.component.html'
})
export class AppWeatherForecastFormInputComponent implements OnInit {
  shouldIncludeTodayValues: string[] = ['Yes', 'No']
  weatherForecastForm!: FormGroup;
  weatherForecastData: any
  submitted!: boolean

  constructor(private formBuilder: FormBuilder,private appWeatherForecastService: AppWeatherForecastService) { }
    
  ngOnInit(): void {
    this.submitted = false;
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
    this.submitted = true;
    this.weatherForecastData = this.weatherForecastForm.value;
    console.log("weatherForecastData : " + this.weatherForecastData);
    if (this.weatherForecastForm.valid) {
      this.appWeatherForecastService.fetchWeatherForecastData(this.weatherForecastData.city, this.weatherForecastData.numberOfDaysToForecast, this.weatherForecastData.shouldIncludeToday = 'No' ? 'false' : 'true');
      this.submitted = false;
    }
  }
}
