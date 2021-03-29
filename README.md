# mastery-cardealership
Final Project from Software Guild boot camp
Requirements: 

Car Dealership website with user role based authentication. Paypal & Google Recaptcha integration. 

Requirements: https://github.com/spitum/mastery-cardealership/blob/main/Car%20Dealership%20Requirements.pdf
Wireframes: https://github.com/spitum/mastery-cardealership/blob/main/Car-Dealership-Wireframes.pdf

# Setup 
Run scripts in DBScripts in following order: database > tables > sprocs > DBReset

Within GuildCars.UI project - update connection string to point to database where above DBScripts were run. 

**For Google Recaptcha setup:**
> Within GuildCar.UI/GoogleCaptcha/SiteSettings.cs - Add secret/site key values.


**For Paypal setup:**
> Within GuildCar.UI/webconfig.cs - Add values for paypal/settings/clientid and paypal/settings/clientSecret 

