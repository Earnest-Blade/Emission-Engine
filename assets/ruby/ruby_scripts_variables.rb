#init_offset=10

# Current app reference.
# This is the main class of the game. This class contain all informations, methods 
# and variables about the project, the game, the window...
$application = Application.CURRENT_APP

# Gui data reference from current app.
# This class contain all informations about gui, ui (window size, window name...).
$gui = $application.getApplicationGUIData

# Application and project data from current app
# This class contain all information about your current project.
$data = $application.getApplicationData
