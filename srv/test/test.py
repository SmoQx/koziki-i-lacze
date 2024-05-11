import socket

# Create a socket object
client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Get the local machine name
host = "127.0.1.1"
port = 55000  # Use the same port as the server

# Connect to the server
client_socket.connect((host, port))

# Send data to the server
message = '{"method": "GET", "message": "user_table"}'
client_socket.send(message.encode())

# Receive a response from the server
response = client_socket.recv(1024).decode()
print(f"Response from server: {response}")

# Close the connection
client_socket.close()
