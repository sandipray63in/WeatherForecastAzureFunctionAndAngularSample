Feature: WeatherForecast
@WeatherForecast
Scenario: Predict Weather Forecast based on supplied data
	Given I supply the values ('<authKey>','<city>', '<numberOfDaysToForecast>','<shouldIncludeToday>')
	When Weather Forecast API Executed
	Then it should return '<statusCode>'

Examples: 
| authKey                              | city    | numberOfDaysToForecast | shouldIncludeToday | statusCode |
| 3faecab1-02e4-42c3-b7f0-11c74499cba5 |         | 3                      | true               | BadRequest |
| 3faecab1-02e4-42c3-b7f0-11c74499cba5 | xyz     | 3                      | true               | BadRequest |
| 3faecab1-02e4-42c3-b7f0-11c74499cba5 | kolkata | 3                      | true               | OK         |