import os
import os.path
import shutil

# Automates running the static ui build and copying
# it into the .Net project.

# TODO: there should be a better way to do this with
#       a watcher on source files.

www_root_dir = "Fracture/wwwroot"
ui_dist_dir  = "fracture-ui/dist"

def build_fracture_ui():
  cdir = os.getcwd()
  try:
    os.chdir("fracture-ui")
    if (os.system("npm run build")):
      raise("Failed to build user interface.")
  finally:
    os.chdir(cdir)

def copy_dist():
  if os.path.isdir(www_root_dir):
    print("Removing old distribution.")
    shutil.rmtree(www_root_dir)
  print("Copying distribution.")
  shutil.copytree(ui_dist_dir, www_root_dir)

build_fracture_ui()
copy_dist()