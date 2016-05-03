# Warm Transfer: Transfer support calls from one agent to another using ASP.NET MVC

[![Build status](https://ci.appveyor.com/api/projects/status/q7jpxx2jvds1hjmy?svg=true)](https://ci.appveyor.com/project/TwilioDevEd/warm-transfer-csharp)

## Local development

1. First clone this repository and `cd` into it.

   ```
   git clone git@github.com:TwilioDevEd/warm-transfer-csharp.git
   cd warm-transfer-csharp
   ```

1. Create the sample configuration file and edit it to match your configuration.

  ```
  rename WarmTransfer.Web\Local.config.example WarmTransfer.Web\Local.config
  ```

 You can find your `TwilioAccountSid` and `TwilioAuthToken` in your
 [Twilio Account Settings](https://www.twilio.com/user/account/settings).
 You will also need a `TwilioPhoneNumber`, you may find it [here](https://www.twilio.com/user/account/phone-numbers/incoming).


1. Create database and run migrations.

   In Visual Studio, open the following command in the Package Manager Console.

   ```
   Update-Database
   ```

   Make sure Sql Server is up and running.

1. Expose your application to the wider internet using [ngrok](http://ngrok.com). This step
  is important because the application won't work as expected if you run it through
  localhost.

  To start using `ngrok` in our project you'll have execute to the following line in the _command prompt_.

  ```
  ngrok http 55585 -host-header="localhost:55585"
  ```

  Keep in mind that our endpoint is:

  ```
  http://<your-ngrok-subdomain>.ngrok.io/Conference/ConnectClient
  ```

  Remember to update the Local.config file with the generated <your-ngrok-subdomain>.

1. Configure Twilio to call your webhooks

  You will also need to configure Twilio to call your application when calls are received on your `TWILIO_NUMBER`. The voice url should look something like this:

  ```
  http://<your-ngrok-subdomain>.ngrok.io/Conference/ConnectClient
  ```

  ![Configure Voice](http://howtodocs.s3.amazonaws.com/twilio-number-config-all-med.gif)


That's it!

## How to Demo

1. Navigate to `https://<ngrok_subdomain>.ngrok.io` in two different
   browser tabs or windows.

   **Notes:**
   * Remember to use your SSL enabled ngrok url `https`.
   Failing to do this won't allow you to receive incoming calls.
   
   * The application has been tested with [Chrome](https://www.google.com/chrome/)
   and [Firefox](https://firefox.com). Safari is not supported at the moment.

1. In one window/tab click `Connect as Agent 1` and in the other one click
   `Connect as Agent 2`. Now both agents are waiting for an incoming call.

1. Dial your [Twilio Number]() to start a call with `Agent 1`. Your `TWILIO_NUMBER`
   environment variable was set when configuring the application to run.

1. When `Agent 1` answers the call from the client, he/she can dial `Agent 2` in
   by clicking on the `Dial agent 2 in` button.

1. Once `Agent 2` answers the call all three participants will have joined the same
   call. After that `Agent 1` can drop the call and leave both the client and `Agent 2`
   having a pleasant talk.

## Meta

* No warranty expressed or implied. Software is as is. Diggity.
* [MIT License](http://www.opensource.org/licenses/mit-license.html)
* Lovingly crafted by Twilio Developer Education.
