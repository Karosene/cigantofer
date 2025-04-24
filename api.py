import requests
from bs4 import BeautifulSoup
import datetime

vinobranieUrls = [
    "https://www.folklorfest.sk/vinobranie-a-vino/",
    "https://www.kudyznudy.cz/aktuality/kam-v-zari-na-vinobrani-nevite-poradime",
]

selectedVinobranieUrls = []

cityList = ["bratislava", "pezinok", "modra", "hlohovec", "sudoměřice", "komárno", "piešťany", "skalica", "vráble", "lozorno", "krťíš", "želovce", "vajnory",
            "krakovany", "košice", "kyjov", "sebechleby", "svätý jur", "nitra", "drnholec", "bzenec", "hustopeče", "mikulov", "hradiště", "pavlovic", "strážnice",
            "milotic", "bojanovic", "sedlec", "znojm"]


def findCities(url):
    response = requests.get(url)
    url = "https://www.folklorfest.sk/vinobranie-a-vino/"
    soup = BeautifulSoup(response.text, "html.parser")
    text = soup.get_text().lower()

    events = soup.find_all("div", class_="event")
    for event in events:
        title = event.find("h3")
        date_element = event.find("span", class_="date")

        if title and date_element:
            raw_date = date_element.text.strip()  # napr. "20.09.2024"
            try:
                event_date = datetime.strptime(raw_date, "%d.%m.%Y")
                today = datetime.today()

                if event_date >= today:
                    print(f"{title.text.strip()} → {raw_date}")
            except ValueError:
                print(f"Nesprávny formát dátumu: {raw_date}")
                

    found_cities = [mesto for mesto in cityList if mesto in text]
    if "znojm" in found_cities:
        found_cities = [city if city != "znojm" else "znojmo" for city in found_cities]    
    if "bojanovic" in found_cities:
        found_cities = [city if city != "bojanovic" else "bojanovice" for city in found_cities]    
    if "milotic" in found_cities:
        found_cities = [city if city != "milotic" else "milotice" for city in found_cities]    
    if "pavlovic" in found_cities:
        found_cities = [city if city != "pavlovic" else "pavlovice" for city in found_cities]    

    return found_cities
    


for website in vinobranieUrls:
    cities = findCities(website)
    if cities:
        selectedVinobranieUrls.append((website, cities))

for url, cities in selectedVinobranieUrls:
    print(f"{url} → {', '.join(cities)}")