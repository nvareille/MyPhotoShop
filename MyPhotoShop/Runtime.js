var Module = 
{
    BufferCanvas: null,
    BufferCtx: null,
    Canvas: null,
    Ctx: null,
    ViewportPtr: null,
    ViewportData: null,
    Painting: false,
    Layers: [],
    ViewportSize: {x: 0, y:0},
    onRuntimeInitialized: () => {
        console.log("Chargé ma GL");
        _SetDebug(true);
    
        Module.Canvas = document.getElementById("canvas");
        Module.Ctx = Module.Canvas.getContext("2d");
        Module.BufferCanvas = document.createElement("canvas");
        Module.BufferCtx = Module.BufferCanvas.getContext("2d");

        document.getElementById("LoadFile").addEventListener("change", Module.LoadFile);
        document.getElementById("grayscale").addEventListener("click", Module.Grayscale);
        document.getElementById("colorPicker").addEventListener("change", Module.ChangeBrushColor);
        document.getElementById("saveImage").addEventListener("click", Module.SaveImage)
        Module.Canvas.addEventListener("mousedown", Module.MouseDown);
        Module.Canvas.addEventListener("mouseup", Module.MouseUp);
        Module.Canvas.addEventListener("mousemove", Module.MoveMouse);

        setInterval(() => Module.DisplayViewport(), 10);
    },
    LoadFile: async (e) =>
    {
        const img = new Image();
        img.src = URL.createObjectURL(e.target.files[0]);
        await img.decode();

        Module.LoadImage(img);
        Module.DisplayViewport();
    },
    ChangeBrushColor(e)
    {
        let value = e.target.value.substring(1, 7);
        let r = parseInt("0x" + value.substring(0, 2), 16);
        let g = parseInt("0x" + value.substring(2, 4), 16);
        let b = parseInt("0x" + value.substring(4, 6), 16);
        
        Module._ChangeBrushColor(r, g, b);
    },
    Grayscale: () =>
    {
        _Grayscale();
        Module.DisplayViewport();
    },
    LoadImage(img)
    {
        let x = img.width;
        let y = img.height;
        
        Module.BufferCanvas.width = x;
        Module.BufferCanvas.height = y;
        Module.BufferCtx.drawImage(img, 0, 0);
        let imageData = Module.BufferCtx.getImageData(0, 0, x, y);
        let ptr = _malloc(x * y * 4);

        HEAPU8.set(imageData.data, ptr);

        Module._LoadImage(ptr, x, y);
    },
    DisplayViewport()
    {
        console.log("Rendu");
        let ptr = _GetViewport();
        let x = _GetViewportSizeX();
        let y = _GetViewportSizeY();

        if (Module.ViewportSize.x != x || Module.ViewportSize.y != y)
        {
            console.log("resize");
            Module.Canvas.width = x;
            Module.Canvas.height = y;
            Module.ViewportSize = {x: x, y: y};

            Module.ViewportData = new ImageData(x, y);
        }
        
        let b = HEAPU8.subarray(ptr, ptr + x * y * 4);

        Module.ViewportData.data.set(b);

        Module.Ctx.putImageData(Module.ViewportData, 0, 0);
    },
    SetImageData: (ptr, x, y) =>
    {
        _LoadImage(ptr, x, y)
    },
    MousePosition(e)
    {
        let rect = Module.Canvas.getBoundingClientRect();
        let x = Math.round(e.clientX - rect.left);
        let y = Math.round(e.clientY - rect.top);

        return ({x, y});
    },
    MouseDown(e)
    {
        let pos = Module.MousePosition(e);
            
        Module.Painting = true;
    },
    MouseUp(e)
    {
        Module.Painting = false;
    },
    MoveMouse(e)
    {
        let pos = Module.MousePosition(e);

        if (Module.Painting)
        {
            Module.ApplyPaint(pos)
        }
    },
    ApplyPaint(pos)
    {
        Module._ApplyPaint(pos.x, pos.y);
        Module.DisplayViewport();
    },
    SaveImage()
    {
        Module.Canvas.toBlob(blob => 
        {
            const link = document.createElement('a');
            link.href = URL.createObjectURL(blob);
            link.download = "image.png";

            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }, 'image/png');
    },
    CreateLayer(x, y)
    {
        _CreateLayer(x, y)
    }
};

