# whatsapp_bot_2025
whatsapp bot for common FAQ running on a RESTful ASP.NET middleware, database in mongoDB, using the whatsapp API.

it should go without saying that we are going to handle API keys, as well as other sensitive info of all kinds here, so we must find a way to keep all of this either private, saved locally, somewhere else, etc. But NEVER push to the public repo. this repo will stay here for educational purposes, both mine and whoever stumbles upon it and finds it useful.

In my limited knowledge ASP.NET seems to be the best way to tackle a simple whatsapp / social media FAQ bot, for no other reason that the entire Azure / Microsoft development environment is massive. So lets try our best at making this work.

I'll update my website, chemiloco.lol, with some sort of blog as I go and discover, fix and destroy, along the way, but if you need to reach out to me please do so at my email jose.vqz.mdz@gmail.com. GMT-6 timezone. Thank you!

Anyways, this thing requires you to run ngrok alongside so the webhook is accesible to the net, since this thing is still being run on localhost. More info here:

https://ngrok.com/

https://ngrok.com/docs/getting-started/

anyways, edit whatsapptests.http so the localhost port reads as:
@whatsapp_tests_HostAddress = http://localhost:8080

and also edit launchSettings.json so the following configs for swagger and such are:

 "profiles": {
   "http": {
     "commandName": "Project",
     "dotnetRunMessages": true,
     "launchBrowser": true,
     "launchUrl": "swagger",
     "applicationUrl": "http://localhost:8080",
     "environmentVariables": {
       "ASPNETCORE_ENVIRONMENT": "Development"
     }
   },
   "https": {
     "commandName": "Project",
     "dotnetRunMessages": true,
     "launchBrowser": true,
     "launchUrl": "swagger",
     "applicationUrl": "https://localhost:7285;http://localhost:8080",
     "environmentVariables": {
       "ASPNETCORE_ENVIRONMENT": "Development"
     }

     this will make it so your localhost webhook API REST blablabla thing is reachable through the internet. All of this I'll write it down in my own .docx file over there in the repo, but I figure it should go here as well just in case anyone reads this (probably not! as no one likes me and i have no frieds haha!!!... :^( )