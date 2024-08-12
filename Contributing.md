# Contributing

Before contributing to this project there are a few important things that should be known  

## General Rules

Each piece of work contributed should link with an [issue](https://github.com/illuminat3/food-tracker-website/issues).  
Each branch should only link to one piece of work.  

## Page Authentication

I have created 3 [base components](https://github.com/illuminat3/food-tracker-website/tree/main/FoodTracker/Pages/BaseComponents).  
They are used to handle authenticating users. 

### UnsecureComponentBase

This is used to handle any pages where the user should be signed out to access.  
Examples of this are the login page or sign up page.

### SecureComponentBase

This is used to handle any pages where the user should be signed in to access.  
Examples of this are the home page.

### AdminComponentBase

This is used to handle any pages where the user should be signed in and admin to access.  
Examples of this are the admin page.
