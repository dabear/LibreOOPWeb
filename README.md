# LibreOOPWeb

This is a site to upload and manage libre readings. 
If you consider setting up an instance of LibreOOPWeb, you should really really know what you are doing, so instructions here are going to brief

## Install LibreOOPWeb
* Use azure, set up a new web app and deploy from source code. Select "github"
* Add the following app settings:
* * NS_Host should point to a nightscout installation, f.ex. https://somesite.herokuapp.com . This will be used to authorize users, both uploaders and processor users. Users should be administered in nightscout's admin tools
* * Mongo_Url should be a mongo uri string to a completely new database, separate from your nightscout install. This will be used to store libre readings

## Setup users in nightscout
* Go to your nightscout site admin tools. Set up two new roles
* * libreoopprocessor - users that are members of this group are allowed to fetch, process and upload processed results
* * libreoop - users that are members of this group are allowed to upload raw readings for processing
* Chttp://glucose.space/reate necessary amount of users as you see fit

## Set up a processing agent
Setting on a processing agent requires you to create one user in nightscout that has membership with libreoopprocessor permissions
* Build a special version of libreoopalgo, it can be found here: https://github.com/dabear/LibreOOPAlgorithm/commit/90e78de0f70fe3c01f2371aa6f7b36e21051abc5
* Configure the LIBRE_OOP_WEB_ENABLE, LIBRE_OOP_WEB_PROCESSING_TOKEN and LIBRE_OOP_WEBSITE variables before building. 
* The details regarding how you should build the oopalgo is out of scope for this document
* oopalgo app needs to be run on a physical android device. Emulators are not an option currently, but if you make it work, please share it with me.

##Known sites
The following sites implements either fully or partially libreoopweb functionality;
* http://libreoopweb.azurewebsites.net/
* http://glucose.space/
