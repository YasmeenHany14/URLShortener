import {Component} from '@angular/core';
import {Router, RouterLink} from '@angular/router';
import {Menubar} from 'primeng/menubar';

@Component({
  selector: 'app-header',
  imports: [
    Menubar,
    RouterLink,
  ],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class Header {
  navLinks: { label: string, routerLink?: any, icon?: string, command?: () => void }[] = [];

  constructor(private router: Router) {
    this.setNavLinks();
  }
  setNavLinks() {
      this.navLinks = [];
  }


  showNavBar(): boolean {
    const route = this.router.url;
    return !(route.startsWith('/login') || route.startsWith('/register'));
  }
}
