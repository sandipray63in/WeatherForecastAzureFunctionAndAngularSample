Feature: WeatherForecast
@WeatherForecast
Scenario: Predict Weather Forecast based on supplied data
	Given I supply the values ('<city>', '<numberOfDaysToForecast>','<shouldIncludeToday>')
	When Weather Forecast API Executed
	Then it should return '<statusCode>'

Examples: 
| city    | numberOfDaysToForecast | shouldIncludeToday | statusCode |
|         | 3                      | true               | BadRequest |
| xyz     | 3                      | true               | BadRequest |
| kolkata | 3                      | true               | OK         |