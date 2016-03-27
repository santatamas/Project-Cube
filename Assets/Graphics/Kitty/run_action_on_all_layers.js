// Get layers in a document
var sourceDocument = app.activeDocument;
var visibleLayers  = [];
var visibleLayers  = collectAllLayers(sourceDocument, visibleLayers);

// Print out total layers found
alert(visibleLayers.length);


// Recursively get all visible art layers in a given document
function collectAllLayers (parent, allLayers)
{
    for (var m = 0; m < parent.layers.length; m++)
    {
        var currentLayer = parent.layers[m];
        if (currentLayer.typename === "ArtLayer")
        {
            if(currentLayer.name.indexOf('skip') > -1)
            continue;
            
            currentLayer.visible = true;
            sourceDocument.activeLayer = currentLayer;
            app.doAction("Recolor", "PixelCat");
            currentLayer.visible = false;

            allLayers.push(currentLayer);
        }
        else
        {
            collectAllLayers(currentLayer, allLayers);
        }
    }
    return allLayers;
}
