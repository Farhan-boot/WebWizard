
'use strict';

// Update cache names any time any of the cached files change.
const CACHE_NAME = 'static-cache-v2';
const DATA_CACHE_NAME = 'data-cache-v1';

// Add list of files to cache here.
const FILES_TO_CACHE = [
  '/',
  '/~/MobileAssets/index.html',
  '/~/MobileAssets/group.html',
  '/~/MobileAssets/status.html',
  '/~/MobileAssets/call-log.html',
  '/~/MobileAssets/login.html',
  '/~/MobileAssets/register.html',
  '/~/MobileAssets/forgot-password.html',
  '/~/MobileAssets/scripts/popper.min.js',
  '/~/MobileAssets/scripts/bootstrap.min.js',
  '/~/MobileAssets/scripts/jquery-3.4.1.min.js',
  '/~/MobileAssets/scripts/plugins/mcustomscroll/jquery.mCustomScrollbar.js',
  '/~/MobileAssets/scripts/app.js',
  '/~/MobileAssets/scripts/install.js',
  '/~/MobileAssets/scripts/luxon-1.11.4.js',
  '/~/MobileAssets/scripts/plugins/mcustomscroll/jquery.mCustomScrollbar.css',
  '/~/MobileAssets/scripts/plugins/fontawesome/css/fontawesome.min.css',
  '/~/MobileAssets/scripts/plugins/fontawesome/css/all.min.css',
  '/~/MobileAssets/styles/bootstrap.min.css',
  '/~/MobileAssets/styles/inline.css',
  '/~/MobileAssets/images/install.svg',
  '/~/MobileAssets/images/avatar-1.jpg',
  '/~/MobileAssets/images/avatar-2.jpg',
  '/~/MobileAssets/images/avatar-3.jpg',
  '/~/MobileAssets/images/avatar-4.jpg',
  '/~/MobileAssets/images/avatar-5.jpg',
  '/~/MobileAssets/images/avatar-6.jpg',
  '/~/MobileAssets/images/avatar-7.jpg',
  '/~/MobileAssets/images/avatar-8.jpg',
  '/~/MobileAssets/images/call-icon.png',
  '/~/MobileAssets/images/carousel.jpg',
  '/~/MobileAssets/images/carousel1.jpg',
  '/~/MobileAssets/images/carousel2.jpg',
  '/~/MobileAssets/images/double-tick.png',
  '/~/MobileAssets/images/incoming-call-icon.svg',
  '/~/MobileAssets/images/logo.png',
  '/~/MobileAssets/images/favicon.ico',
  '/~/MobileAssets/images/media1.jpg',
  '/~/MobileAssets/images/media2.jpg',
  '/~/MobileAssets/images/media3.jpg',
  '/~/images/missed-call-icon.svg',
  '/~/MobileAssets/images/status-icon.png',
  '/~/MobileAssets/images/top-curve-bg.png',
  '/~/MobileAssets/images/screenshot-icon.png',
  '/~/MobileAssets/images/call-close.svg',
  '/~/MobileAssets/fonts/lato-regular.woff2',
  '/~/MobileAssets/fonts/lato-regular.woff',
  '/~/MobileAssets/fonts/lato-bold.woff2',
  '/~/MobileAssets/fonts/lato-bold.woff',
  '/~/MobileAssets/fonts/lato-black.woff2',
  '/~/MobileAssets/fonts/lato-black.woff',
  '/~/MobileAssets/scripts/plugins/fontawesome/webfonts/fa-solid-900.woff2',
  '/~/MobileAssets/scripts/plugins/fontawesome/webfonts/fa-solid-900.woff',
  '/~/MobileAssets/scripts/plugins/fontawesome/webfonts/fa-solid-900.ttf',
  '/~/MobileAssets/scripts/plugins/fontawesome/webfonts/fa-regular-400.woff2',
  '/~/MobileAssets/scripts/plugins/fontawesome/webfonts/fa-regular-400.woff',
  '/~/MobileAssets/scripts/plugins/fontawesome/webfonts/fa-regular-400.ttf',
  '/~/MobileAssets/scripts/plugins/fontawesome/webfonts/fa-brands-400.woff2',
  '/~/MobileAssets/scripts/plugins/fontawesome/webfonts/fa-brands-400.woff',
  '/~/MobileAssets/scripts/plugins/fontawesome/webfonts/fa-brands-400.ttf',

];

self.addEventListener('install', (evt) => {
  console.log('[ServiceWorker] Install');
  // Precache static resources here.
  evt.waitUntil(
    caches.open(CACHE_NAME).then((cache) => {
      console.log('[ServiceWorker] Pre-caching offline page');
      return cache.addAll(FILES_TO_CACHE);
    })
  );
  self.skipWaiting();
});

self.addEventListener('activate', (evt) => {
  console.log('[ServiceWorker] Activate');
  // Remove previous cached data from disk.
  evt.waitUntil(
      caches.keys().then((keyList) => {
        return Promise.all(keyList.map((key) => {
          if (key !== CACHE_NAME && key !== DATA_CACHE_NAME) {
            console.log('[ServiceWorker] Removing old cache', key);
            return caches.delete(key);
          }
        }));
      })
  );
  self.clients.claim();
});

self.addEventListener('fetch', (evt) => {
  console.log('[ServiceWorker] Fetch', evt.request.url);
  // Add fetch event handler here.
  if (evt.request.url.includes('/forecast/')) {
  console.log('[Service Worker] Fetch (data)', evt.request.url);
  evt.respondWith(
      caches.open(DATA_CACHE_NAME).then((cache) => {
        return fetch(evt.request)
            .then((response) => {
              // If the response was good, clone it and store it in the cache.
              if (response.status === 200) {
                cache.put(evt.request.url, response.clone());
              }
              return response;
            }).catch((err) => {
              // Network request failed, try to get it from the cache.
              return cache.match(evt.request);
            });
      }));
  return;
}
evt.respondWith(
    caches.open(CACHE_NAME).then((cache) => {
      return cache.match(evt.request)
          .then((response) => {
            return response || fetch(evt.request);
          });
    })
);

});
