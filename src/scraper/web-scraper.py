from selenium import webdriver
from selenium.webdriver.common.by import By
import time
import requests
from PIL import Image
import pytesseract
import cv2
import numpy as np

# Set up Selenium
driver = webdriver.Chrome(executable_path='/path/to/chromedriver')

# Log in to Facebook
driver.get('https://www.facebook.com')
time.sleep(3)

email_input = driver.find_element(By.ID, 'email')
password_input = driver.find_element(By.ID, 'pass')

email_input.send_keys('your-email')
password_input.send_keys('your-password')

login_button = driver.find_element(By.NAME, 'login')
login_button.click()

# Wait for login to complete
time.sleep(5)

# Navigate to the photos page
driver.get('https://www.facebook.com/lacantina.center/photos_by')
time.sleep(5)

# Scroll down to load more photos if necessary
# for _ in range(5):
#     driver.execute_script("window.scrollTo(0, document.body.scrollHeight);")
#     time.sleep(3)

# Find image elements
image_elements = driver.find_elements(By.CSS_SELECTOR, 'img')
image_urls = [img.get_attribute('src') for img in image_elements]

# Download images and perform OCR
image_texts = []
for url in image_urls:
    response = requests.get(url, stream=True)
    img = Image.open(response.raw).convert('RGB')
    text = pytesseract.image_to_string(img)
    image_texts.append((url, text))

# Find the image with the most text
max_text_image = max(image_texts, key=lambda x: len(x[1]))

print("Image URL with the most text:", max_text_image[0])
print("Extracted Text:", max_text_image[1])

# Clean up
driver.quit()
