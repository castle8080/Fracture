
import datetime
import os
import os.path
import shutil
import time
import sys


# Automates running the static ui build and copying
# it into the .Net project.

try:
  import watchdog
  import watchdog.observers

  class CallbackHandler(watchdog.events.FileSystemEventHandler):
    def __init__(self, callback):
      self.callback = callback
    def on_any_event(self, e):
      self.callback(e)
except:
  print("Couldn't load watchdog.")


class UIBuilder:

  def __init__(self):
    self.www_root_dir = "Fracture/wwwroot"
    self.ui_dist_dir  = "fracture-ui/dist"
    self.ui_src_dir = "fracture-ui/src"
    self.src_dirty = True

  def build_fracture_ui(self):
    cdir = os.getcwd()
    try:
      os.chdir("fracture-ui")
      if (os.system("npm run build")):
        raise("Failed to build user interface.")
    finally:
      os.chdir(cdir)

  def copy_dist(self):
    if os.path.isdir(self.www_root_dir):
      print("Removing old distribution.")
      shutil.rmtree(self.www_root_dir, True)
    print("Copying distribution.")
    shutil.copytree(self.ui_dist_dir, self.www_root_dir)

  def build(self):
    start_time = datetime.datetime.now()
    self.build_fracture_ui()
    exec_time = datetime.datetime.now() - start_time
    print("Build Complete[{}]: Seconds {}".format(datetime.datetime.now().isoformat(), exec_time.total_seconds()))

    self.copy_dist()

  def do_build(self):
    self.build()
    self.src_dirty = False

  def watch(self):
    print("Running in watch mode.")
    event_handler = CallbackHandler(lambda e: self.on_fs_event(e))
    observer = watchdog.observers.Observer()
    observer.schedule(event_handler, self.ui_src_dir, recursive=True)
    observer.start()
    try:
      while True:
        time.sleep(2)
        if self.src_dirty:
          self.do_build()
    except KeyboardInterrupt:
      observer.stop()
    observer.join()

  def on_fs_event(self, e):
    self.src_dirty = True

  def run(self):
    watch_mode = False
    for i in range(1, len(sys.argv)):
      arg = sys.argv[i]
      if arg == '-w' or arg == '--watch':
        watch_mode = True
    if watch_mode:
      self.watch()
    else:
      self.build()

UIBuilder().run()
