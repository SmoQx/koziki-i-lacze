import socket
import hashlib


def main(mesage: dict, host: str = "127.0.1.1", port: int = 55000,):
# Create a socket object
    client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Connect to the server
    client_socket.connect((host, port))

# Send data to the server
    message = str(mesage)
    client_socket.send(message.encode())

# Receive a response from the server
    response = client_socket.recv(1024).decode()
    print(f"Response from server: {response}")

# Close the connection
    client_socket.close()


def calc_hash_sha256(text: str):
    hasher = hashlib.sha256()
    hasher.update(text.encode("utf-8"))
    hashed_string = hasher.hexdigest()

    return hashed_string


if __name__ == "__main__":
#    message = {"method": "GET", "message": "user_table"}
#    main(message)
#    echo = {"method": "echo", "message": "user_table"}
#    main(echo)
#    greet = {"method": "greet", "message": "user_table"}
#    main(greet)
#    message = {"method": "GET", "message": {"user_name": "usr", "password": "password"}}
#    main(message)
#    add_user = {"method": "GET", "message": {"user_name": "username", "password": "password"}}
#    main(add_user)
#    user_info = {"method": "GET", "message": {"player_name": "nickname"}}
#    main(user_info)
    add_character = {"method": "PUT", "message": {"new_character": "yes", "Nickname": "name", "UserName": f'{calc_hash_sha256("username")}'}}
    main(add_character)
