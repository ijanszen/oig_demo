# oig_demo

Hoe zou je de technische inrichting maken om een respondent de vragenlijst te tonen?
	-Questions model, controller & vragenlijst opzettten
	-Deze combinatie met surveyJS voor de verschillende soorten vragen ziet er veelbelovend uit
	 https://www.jerriepelser.com/blog/using-surveyjs-with-aspnet-core/
	-Waarschijnlijk een noSQL achtige database opzetten om de antwoorden in op te slaan
		- omdat antwoorddata flink verschillend gaat zijn en ACID niet echt nodig nodig is
	- Zorgen dat vragenlijsten tijdelijk worden opgeslagen bij verlaten
	- bij grote vragenlijsten progress bars zodat je weet hoe ver je bent in de vragenlijst

Welke gegevens gebruik je hierbij? 
	- Soort vraag
	- Gebruiker die hem invult 
	- Owner die hem heeft aangemaakt
	- Vragen zelf
	- status (compleet | niet compleet ), je wilt wss geen halve vragenlijsten in je onderzoek

Hoe communiceer je naar de respondent? 
	- Als de respondent een account heeft dan kan er op zijn hoofdpagina alle informatie getoont worden die nodig is. 
	- Wel zou ik mailen als er een nieuw onderzoek beschikbaar is. Bijna iedereen checkt mail, niet iedereen checkt applicaties voor updates. 
 
Soms zijn niet alle respondenten vantevoren in het systeem geregistreerd, bijvoorbeeld als je een onderzoek wilt afnemen via een link op een website. 
Hoe zou je in dat geval bovenstaande vraag beantwoorden?
	- inditgeval zou ik vragen om een email waarmee naar de correspondent gecommuniceerd kan worden. 
	eventueel kunnen nummers / social media als alternatief gebruikt worden maar ik denk dat email het meest toegankelijk is. 
