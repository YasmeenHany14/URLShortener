import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {Header} from './layout/header/header';
import {Toast} from 'primeng/toast';
import {HomePage} from './pages/home-page/home-page';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header, Toast, HomePage],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('src');
}
