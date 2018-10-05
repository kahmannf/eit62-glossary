import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { EntryComponent } from './entry/entry.component';
import { EntryListComponent } from './entry-list/entry-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    EntryComponent,
    EntryListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
