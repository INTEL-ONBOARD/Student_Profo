# import requests
# from bs4 import BeautifulSoup
# import json

# def get_dump(url, path):
#     full_url = url + path

#     headers = {
#         'Content-Type': 'application/x-www-form-urlencoded',
#         'Cookie': '_ga=GA1.2.823271897.1721246274; _gid=GA1.2.2047118751.1721246274; twk_idm_key=uI4l4is7BBP4bHh7gB8f0; _gat=1; _ga_8Y39KNRBM5=GS1.2.1721259757.4.1.1721259798.19.0.782704203; TawkConnectionTime=0; twk_uuid_5f6c82264704467e89f1ee75=%7B%22uuid%22%3A%221.92OoxUrMGNfGvbmd4OAARBZLGcTlluRC4aKi61QtgVhqtx0U6Cyw7UV5ZrTzTp7JXEFxcHKukVB5cJuqL4q3eoBlGz3nZaYpDLv3CKCDJLePPrYaL04I6KjM4GUq%22%2C%22version%22%3A3%2C%22domain%22%3A%22nibmworldwide.com%22%2C%22ts%22%3A1721259808071%7D',
#         'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.6478.127 Safari/537.36'
#     }

#     data = {
#         'F[Programme]': '2591',
#         'F[Batch]': '6960',
#         'F[Student]': '83664',
#         'CK': '5368990aafbd18eb3dfb2a333445ecb9d5f5be97'
#     }

#     response = requests.post(full_url, headers=headers, data=data)
#     html_content = response.content

#     soup = BeautifulSoup(html_content, 'html.parser')

#     jsonObject = {}

#     jsonObject["Programmes"] = extract_options(soup, 'select[name="F[Programme]"]')
#     jsonObject["Batches"] = extract_options(soup, 'select[name="F[Batch]"]')
#     jsonObject["Students"] = extract_options(soup, 'select[name="F[Student]"]')

#     # Extract table data
#     table_data = []
#     table_rows = soup.select('table.dg.c tbody tr')
#     for row in table_rows:
#         cells = row.find_all('td')
#         if cells:
#             row_object = {
#                 "Number": cells[0].get_text(strip=True),
#                 "Subject": cells[1].get_text(strip=True),
#                 "Special": cells[2].get_text(strip=True),
#                 "ExamDate": cells[3].get_text(strip=True),
#                 "CourseWork": cells[4].get_text(strip=True),
#                 "Exam": cells[5].get_text(strip=True),
#                 "FinalGrade": cells[6].get_text(strip=True),
#                 "Points": cells[7].get_text(strip=True)
#             }
#             table_data.append(row_object)
#     jsonObject["TableData"] = table_data

#     print(json.dumps(jsonObject, indent=2))
#     return jsonObject

# def extract_options(soup, select_css):
#     select = soup.select_one(select_css)
#     if select:
#         options_array = []
#         options = select.find_all('option')
#         for option in options:
#             option_value = option.get('value', '')
#             option_text = option.get_text(strip=True)
#             option_object = {
#                 "value": option_value,
#                 "text": option_text
#             }
#             options_array.append(option_object)
#         return options_array
#     return []

# # Usage
# url = "https://www.nibmworldwide.com"
# path = "/exams/mis"
# get_dump(url, path)


import requests
from bs4 import BeautifulSoup
import json

def get_programmes(url, path):
    full_url = url + path

    headers = {
        'Content-Type': 'application/x-www-form-urlencoded',
        'Cookie': '_ga=GA1.2.823271897.1721246274; _gid=GA1.2.2047118751.1721246274; twk_idm_key=uI4l4is7BBP4bHh7gB8f0; _gat=1; _ga_8Y39KNRBM5=GS1.2.1721259757.4.1.1721259798.19.0.782704203; TawkConnectionTime=0; twk_uuid_5f6c82264704467e89f1ee75=%7B%22uuid%22%3A%221.92OoxUrMGNfGvbmd4OAARBZLGcTlluRC4aKi61QtgVhqtx0U6Cyw7UV5ZrTzTp7JXEFxcHKukVB5cJuqL4q3eoBlGz3nZaYpDLv3CKCDJLePPrYaL04I6KjM4GUq%22%2C%22version%22%3A3%2C%22domain%22%3A%22nibmworldwide.com%22%2C%22ts%22%3A1721259808071%7D',
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.6478.127 Safari/537.36'
    }

    data = {
        'F[Programme]': '2591',
        'F[Batch]': '6960',
        'F[Student]': '83664',
        'CK': '5368990aafbd18eb3dfb2a333445ecb9d5f5be97'
    }

    response = requests.post(full_url, headers=headers, data=data)
    html_content = response.content

    soup = BeautifulSoup(html_content, 'html.parser')

    programmes = extract_options(soup, 'select[name="F[Programme]"]')

    # Save programmes to a JSON file
    with open('programmes.json', 'w') as json_file:
        json.dump(programmes, json_file, indent=2)

    print("Programmes saved to programmes.json")

def extract_options(soup, select_css):
    select = soup.select_one(select_css)
    if select:
        options_array = []
        options = select.find_all('option')
        for option in options:
            option_value = option.get('value', '')
            option_text = option.get_text(strip=True)
            option_object = {
                "value": option_value,
                "text": option_text
            }
            options_array.append(option_object)
        return options_array
    return []

# Usage
url = "https://www.nibmworldwide.com"
path = "/exams/mis"
get_programmes(url, path)
