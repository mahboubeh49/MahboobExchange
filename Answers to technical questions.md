1. How long did you spend on the coding assignment? What would you add to your solution if you had more time? If you didn't spend much time on the coding assignment then use this as an opportunity to explain what you would add.

I spend almost 5 days on a part-time basis for this task.
•	If I had more time I would add authentication ability to swagger in my Endpoint, because after applying authorization in my end points I was not able to call my endpoint within swagger anymore (it returns 401 which is correct now) and I had to use postman.
•	If I had more time for sure I created more tests with more scenarios
•	In UI project i didn't have time to add tests and some validations which with more time would add tests and precise validation to the Web app.
•	Recently I have used SpecFlow to write nice behavioural tests and endpoint tests, in a more readable way. If I had more time I could create a project and use specflow to test endpoints.
•	If I had more time I could use docker and docker-compose to put every single project in a container and manage them in docker-compose (Unfortunately I still haven't worked with another type of orchestration)
•	If I had more time I would use Angular as a UI framework which is more friendly and I have more experience in it.
•	If I had more time I would use elastic search to use it as a logs container and also Kibana to show logs and report them.
•	If I had more time I would use some policy tools like Polly to manage some connectivity problems like time out and circuit breakers.
•	Although it seems like over engineering but I could use the event mechanism here as well and use RabbitMq to handle communication between services.
-----------------------------
2. What was the most useful feature that was added to the latest version of your language of choice?

•	.Net mouvi : it is  cross platform framework for creating mobile and desktop app Using .NET MAUI, you can develop apps that can run on Android, iOS, macOS, and Windows from a single shared code-base.
•	Record: in C# 9 introduce new feature of reference type .C# 9 introduces records, a new reference type that you can create instead of classes or structs. C# 10 adds record structs so that you can define records as value types. Record types are immutable. you can not change them. 
•	Furl : it is a simple and fluent and testable library to call any type of Endpoint
•	RichardSzalay.MockHttp: I recently got family with is Library mock any kind of HTTP request in a very simple and fluent way
-----------------------------
3. How would you track down a performance issue in production? Have you ever had to do this?

At first, I just tried to figure out where is the source of the problem maybe the database maybe the application in production or maybe third party application.
in each case, I had a couple of solutions to go deep dive and find the problem. here was asked about production so in production at first, I try to figure it out where is the source of the problem. The first tools for me are my logs and my timing and any type of exception that was thrown in production.
On the other hand, there are a few tools like MiniProfiler and dotTrace and dotmemory which were recommended by Microsoft 
-----------------------------
4. What was the latest technical book you have read or tech conference you have been to? What did you learn?

I read  .Net Microservice architecture for .net applications for .net application with was written by Microsoft.
I really love that book because it faced me with lots of architectural patterns and solution which was followed by Microsoft. it also explains lots of details about the internal architecture and some solutions to face with the technical complexity of applications 
-----------------------------
5. What do you think about this technical assessment?

 First of all, I really like this assessment. Although at the first glance it seems like a simple task but in the implementation, I faced with a couple of problems that I should have solved. So it forced me to create a clean, readable, testable, and secure applications with test coverage which is a standard way to follow best practises to create application, so although I had limit of time I tried to do my best to create an application from scratch and put all of the business logics into it
-----------------------------
6. Please, describe yourself using JSON.

{
  "firstName": "Mahboubeh",
  "lastName": "Pourniazi",
  "age": 34,
  "isSingle": false,
  "mobile": "+989196360563",
  "traits": {
    "hairColor": "blond",
    "height": "172"
  },
  "birthdate": "1988-06-19",
  "address": {
    "house": {
      "houseNo": 22,
      "street": "College Ave East",
      "city": "Tehran"
    },
    "postcalCode": 878748
  },
  "ّFriends": [
    "Neda",
    "Ali",
    "Maryam"
  ],
  "strength": [
    "quick learner",
    "motivated",
    "friendly",
    "independent",
    "passionate to learn more"
  ]
}
-----------------------------
About the Project
I did this project with DotNet 6 and Visual Studio 2022. 
It should start normally just by running project in VS. Just kindly keep in your mind, According to use three separated API in the solution(Exchange.Api, Identity and WebAPP) I selected Multiple-Startup project in solution setting. So in start project If you face with a problem just make sure this three project are selected and started correctly together.
Please Also keep in your mind as I understood CoinCapMarket in free API version mode, doesn't provide multiple conversions, so I had to send 5 requests to the CoinCapMarket for each user request as you can see in the below code





