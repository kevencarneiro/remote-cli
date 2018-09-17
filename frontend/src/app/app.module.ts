import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MachineListComponent } from './components/machine-list/machine-list.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule, MatCardModule, MatDividerModule, MatExpansionModule, MatToolbarModule} from '@angular/material';
import {NgMathPipesModule} from 'angular-pipes';
import { CommandInterfaceComponent } from './components/command-interface/command-interface.component';
import {FormsModule} from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    MachineListComponent,
    CommandInterfaceComponent
  ],
  imports: [
    NgMathPipesModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    MatButtonModule,
    MatCardModule,
    MatDividerModule,
    MatExpansionModule,
    MatToolbarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
