import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import Aura from '@primeuix/themes/aura';
import { routes } from './app.routes';
import { providePrimeNG } from "primeng/config";
import {definePreset} from '@primeuix/themes';


const MyPreset = definePreset(Aura, {
  semantic: {
    primary: {
      50:  '{pink.50}',
      100: '{pink.100}',
      200: '{pink.200}',
      300: '{pink.300}',
      400: '{pink.400}',
      500: '{pink.500}',
      600: '{pink.600}',
      700: '{pink.700}',
      800: '{pink.800}',
      900: '{pink.900}',
      950: '{pink.950}'
    },
    colorScheme: {
      light: {
        primary: {
          color:        '{pink.500}',
          inverseColor: '#ffffff',
          hoverColor:   '{pink.600}',
          activeColor:  '{pink.700}'
        },
        highlight: {
          background:   '{pink.100}',
          focusBackground: '{pink.200}',
          color:        '{pink.800}',
          focusColor:   '{pink.900}'
        },
        surface: {
          0:   '#ffffff',
          50:  '{purple.50}',
          100: '{purple.100}',
          200: '{purple.200}',
          300: '{purple.300}',
          400: '{purple.400}',
          500: '{purple.500}',
          600: '{purple.600}',
          700: '{purple.700}',
          800: '{purple.800}',
          900: '{purple.900}',
          950: '{purple.950}'
        }
      },
      dark: {
        primary: {
          color:        '{pink.400}',
          inverseColor: '{purple.950}',
          hoverColor:   '{pink.300}',
          activeColor:  '{pink.200}'
        },
        highlight: {
          background:   'color-mix(in srgb, {pink.400}, transparent 84%)',
          focusBackground: 'color-mix(in srgb, {pink.400}, transparent 76%)',
          color:        'rgba(255,255,255,.87)',
          focusColor:   'rgba(255,255,255,.87)'
        },
        surface: {
          0:   '#ffffff',
          50:  '{purple.950}',
          100: '{purple.900}',
          200: '{purple.800}',
          300: '{purple.700}',
          400: '{purple.600}',
          500: '{purple.500}',
          600: '{purple.400}',
          700: '{purple.300}',
          800: '{purple.200}',
          900: '{purple.100}',
          950: '{purple.50}'
        }
      }
    }
  }
});

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    providePrimeNG({
      theme: {
        preset: MyPreset,
        options: {
          darkModeSelector: '.dark-mode'
        }
      }
    })
  ]
};
