
# Introduction
#### Annual Event Recipe Blog

The AERB (Annual Event Recipe Blog) is a software application which lets multiple users share their recipes to eachother. The users can login, save their work, search other recipes and save said recipe to a favourite list.
## Audience

The AERB is aimed at people wanting to organize events such as parties for Halloween or Christmas or Birthdays or gatherings.


For this we'll implement the following features
    - giving item prices
        Since we're focused on events with a large number of people the total price of a recipe depending on serving could be important to users
    - item weights
        Often for gatherings people will meet at one place, knowing the weight of what you might be bringing is important. If something is too big / heavy somebody might not be able to bring it with them
    - Dietary restrictions and alternatives
        For large events, inclusivity for people who cant eat certain foods due to allergies or religious reasons is important. Due to this, we'll include a section for giving alternative recipes or substitute ingredients
    - (TBD) recommended recipes
        We may include a section of viewed blogs that suggests other similar recipes based on type, serving size etc.


## Functionalities

- Account creation / deletion
- Account info update
- Password Hashing (optional)
- Password updating
- Profile pictures
- Recipe ownerships
- Recipe searching/saving
- Sort Recipe by rating

## How to run the project

## Dotnet dependencies for DB

dotnet tool install --global dotnet-ef --version 6

dotnet add package Microsoft.EntityFrameworkCore -f "net6.0"

dotnet add package Microsoft.EntityFrameworkCore.Design -f
"net6.0" -v 7

dotnet add package Oracle.EntityFrameworkCore -f "net6.0" -v 7.21.9


## Agreements
    Procedures to ensure code functionality
        ensure that any code runs without errors I.E. pressing the run/debug should work and not crash the program
    
    Testing
        unit tests for implemented method / what we determine needs to have a specific unit test

    divide
        divided per class / database implementation
        write unit tests for someone elses classes, each person is given another person who's code they need to write tests for
        if things get difficult class divide may change
        more complex tasks can be done by more people

    ensure robustness
        unit tests
        validation, make sure that the user's inputs are valid. if they arent then let them know
    
    ensure stand alone classes
        by dividing the architectural layers
        make sure each method has a unique function
        testing classes individually

## extra features
    giving item prices (api)
    item weights
    alternatives (for allergies,dietary restrictions) - give another recipe or a substitute ingredient


    maybe: suggestions for searches / recommended recipe list based on written recipes


# classes

## user
Everything related to a user including creating / deleting and user actions is in here

## search
Everything related to searching for the perfect recipe is in here. Things such as sorting by keyword, rating, tags...

## Profile
Everything related to a user profile is in here. Things such as a profile picture, a description, their favourites...

### Recipe info
A namespace which contains everything related to recipes. Things such as ingredients, recipe tags and recipe instructions are located in here

### Sorters
A namespace which contains everything related to sortings your recipes. It helps the user find a recipe based on a specific condition. We can sort based on Ingredients, ratings, serving size and time to cook. 

### Comparators
A namespace which contains everything related to comparing 2 recipes. You can compare 2 recipes based on ratings, servings, time to cook. It is used by Sorters to sort the recipe efficently.


## API
https://github.com/yZipperer/item-api
http://10.172.19.128:8080/

