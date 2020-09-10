# TempLog

Created by: Christopher N. Sefcik on 9/3/2020
Version 2.0: 9/10/2020

### Summary:
The application allows a user to log a name and a temperature using voice commands. 

### Setup:
1) Navigate to the Setup1 folder and find Install.msi
2) Select Install.msi to begin installation
3) Allow installation to complete. Program may need to be whitelisted on antivirus software as it is unsigned by a trusted vendor
4) By default, TempLog creates a textfile called "Temperature Log.txt" and places it in the user's Documents folder
	* To change the location of "Temperature Log.txt", update "TempLog_Config" with the new UNC path
	* To disable this feature, navigate to "TempLog_Config" and set tempLog_on=off
5) By default, email functionality is disabled. To enable email funcionality:
	* Navigate to the email_config file
	* Set email_on=on
	* Update the remaining settings with correct email configuration
	* Email functionality will need to be allowed to bypass antivirus/firewall
6) By default, Timeout feature is set to a 30 second timeout
	* To change timeout rate, set variable 'timeout' in TempLog_Config to the appropriate number of seconds

### Configuration:
1) *Email_Config* : Configures email settings for sending name and temperature to a specified email address. File is setup as follows:
		
		* email_on=(on/off)
		* server=(server address: ex. smtp.google.com)
		* port=(port number)
		* email_username=(email account username)
		* email_password=(email account password)
		* sender_email_address=(sender email address)
		* sender_name=(sender name)
		* subject=(email subject)
		* recipient_email_address=(recipient email address)
		
2) *TempCmds_Config* : Contains a list of temperatures recognized by the program. Default configuration recognizes 0-125 in increments of .1

3) *TempLog_Config* : Allows user to turn logging feature on/off; Contains the location of "TemperatureLog"; Allows user to set timeout feature duration 

		* tempLog_on=(on/off)
		* Path = 
			* Local Path Example: C:\\users\\bob\\TempLog.txt
			* Network Path Example: \\\\NetworkShare\\desktop\\TempLog.txt
		* timeout=(duration)

4) *UsernameCmds_Config* : Contains a list of names recognized by the program. No Default Configuration. User must load names in a text file with one name per line:

		* Tom Hanks
		* Bill Murray
		* Tom Cruise

5) *Path_Config*: Contains the location of the configuration files. UNC paths may be changed if configuration files are moved. File is setup as follows:

		* tempLog_config=Configuration\\tempLog_config.txt
		* usernameCmds_config=Configuration\\usernameCmds_config.txt
		* tempCmds_config=Configuration\\tempCmds_config.txt
		* email_config=Configuration\\email_config.txt
