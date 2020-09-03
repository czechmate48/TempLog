# Temp_Log_Dictator

Created by: Christopher N. Sefcik on 9/3/2020

### Summary:
The application allows a user to log a name and a temperature using voice commands. 

### Setup:
1) Create a directory on the filesystem named "TempLog"
2) Navigate to TempLogDictation\TempLogDictation\bin\Debug
3) Copy TempLogDictation.exe and "Configuration" directory
4) Paste TempLogDictation.exe & "Configuration" directory in "TempLog" directory
5) Setup configuration files [See configuration]
6) Launch program

### Configuration:
1) *Email_Config* : Configures email settings for sending name and temperature to a specified email address. File is setup as follows:
		
		* Email on/off
		* Server
		* Port
		* Sending Account Username
		* Sending Account Password
		* Sending Email Address
		* Subject
		* Receiving Email Address
		
2) *TempCmds_Config* : Contains a list of temperatures recognized by the program. Default configuration recognizes 0-125 in increments of .1

3) *TempLog_Config* : Contains the path where the TempLog will be stored. Requires formatting as would appear in code as well as filename.

		* Path
			* Local Path Example: C:\\users\\bob\\TempLog.txt 
			* Network Path Example: \\\\NetworkShare\\desktop\\TempLog.txt
		* Local Save on/off

4) *UsernameCmds_Config* : Contains a list of names recognized by the program. No Default Configuration
