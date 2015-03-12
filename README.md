PnetLinkLandmarkToolkit
=======================

Landmark Management Tools for the PnetLink system


Requirements:
	- downloadxml.js - https://code.google.com/p/frisaporceddhu/source/browse/trunk/js/downloadxml.js?spec=svn45&r=45
	- jquery.js (Currently using 2.1.1) - http://jquery.com/
	
	- Markericons - I used the ones found here.  http://mapicons.nicolasmollet.com/


Notes on operation:

- JSON docs
	There are 2 example JSON docs contained herein.  Ultimately these are generated by a web service which I will work on recreating
	so it does not use any ancillary data systems such as mine currently does.  To simplify the javascript code I have one JSON "schema"
	that I can use for both individual landmarks as well as for GPS trails.  I just leave the unnecessary fields blank.  Then I can create
	a single JSON that contains both landmark and gps trail data for consumption by the javascript.
- Stop Profiles
	The stopprofiles.xml doc contains pre-defined stop profiles (You'll have to change for your particular stop profiles)
	These are used to make the circles larger/smaller.  The javascript tool does NOT show the arriving ring.  We don't use
	it at present.  When the user clicks the "update" link in the landmark balloon it will pass the stop profile ID that is
	selected.
- UpdateLandmark.aspx
	This is the page that the "update" link in the landmark balloon calls.  This is a code snippet that can be used to create a simple
	c# web page that will talk to the link.
- Landmark Categories.  column - lmtype  table - pnet.landmarks
	By default PnetLink offers a handful of categories that can be assigned to a landmark.  We chose to use these to indicate
	what type of landmark it would be.  Other people may have determined some other use for these fields.  In the code that 
	generates the JSON docs I add a category based on the lmtype.  IE lmtype=1 => "store"


8/22/2014 - Added HTML file, sample JSON documents, and Code-Behind for a C# page to update the landmark.  Updated readme
