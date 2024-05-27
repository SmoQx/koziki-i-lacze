import os
import git
from datetime import datetime
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler

# Function to initialize a Git repository if it doesn't exist
def init_repository(repo_dir):
    if not os.path.exists(os.path.join(repo_dir, '.git')):
        repo = git.Repo.init(repo_dir)
        print("Initialized empty Git repository in", repo_dir)
    else:
        repo = git.Repo(repo_dir)
        print("Git repository already exists in", repo_dir)
    return repo

# Function to add and commit files with file name and date
def commit_files(repo):
    repo.git.add(A=True)
    repo.index.commit(f"{datetime.today()}")

# Watchdog event handler class
class GitChangeHandler(FileSystemEventHandler):
    def __init__(self, repo):
        super().__init__()
        self.repo = repo

    def on_modified(self, event):
        if not event.is_directory:
            commit_files(self.repo)

# Main function
def main(repo_dir):
    repo = init_repository(repo_dir)
    commit_files(repo)

    # Set up watchdog to monitor directory for changes
    event_handler = GitChangeHandler(repo)
    observer = Observer()
    observer.schedule(event_handler, repo_dir, recursive=True)
    observer.start()

    try:
        while True:
            pass
    except KeyboardInterrupt:
        observer.stop()
    observer.join()

if __name__ == "__main__":
    directory_path = input("Enter the directory path: ")
    main(directory_path)
