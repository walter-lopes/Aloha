from hashlib import sha256

if __name__ == '__main__':
    unique_key = CUSTOMER_ID + SMART_LINK
    hashedWord = sha256(unique_key.encode('utf-8')).hexdigest()
    print(hashedWord)