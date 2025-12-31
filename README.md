# Image Merger - Blazor WebAssembly PWA

A simple, **client-side only**, privacy-focused Progressive Web App (PWA) that lets you merge two images (side-by-side or top-bottom) directly in your browser.

**No images are uploaded to any server** — everything happens locally in your browser.

## Features

- Merge two images horizontally or vertically
- Adjustable spacing between images
- Configurable alignment (start, center, end)
- Custom background color or transparent background
- Preview of both input images
- Swap, clear, and download the merged result as PNG
- Fully offline-capable Progressive Web App (PWA)
- Supports large images up to **20 MB** each
- Built with **Blazor WebAssembly** on **.NET 10**

## Live Demo

(To be released)

## Technology Stack

- .NET 10
- Blazor WebAssembly Standalone App
- Progressive Web App (PWA) support
- Canvas-based image merging via JavaScript interop
- No backend, no data collection → maximum privacy

## How to Use

1. Open the app in your browser
2. Click "Choose Image A" and select the first image (max 20 MB)
3. Click "Choose Image B" and select the second image (max 20 MB)
4. Adjust options:
   - Orientation (Vertical / Horizontal)
   - Spacing between images
   - Alignment
   - Background color or transparent
5. Click **Merge Images**
6. Preview the result and click **Download** to save as `merged.png`

You can swap or clear images at any time.

## Installation as PWA

- Open the site on a mobile or desktop browser (Chrome, Edge, Safari, Firefox)
- Click the install icon in the address bar or use "Add to Home Screen"
- The app will work offline after installation

## Running Locally

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Steps

```bash
# Clone the repository
git clone https://github.com/yourusername/image-merger-blazor.git
cd image-merger-blazor

# Restore and build
dotnet restore
dotnet build

# Run the app
dotnet watch run