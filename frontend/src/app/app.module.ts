import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MachineListComponent } from './components/machine-list/machine-list.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {
  MatButtonModule,
  MatCardModule,
  MatDialogModule,
  MatDividerModule,
  MatExpansionModule, MatInputModule,
  MatSelect, MatSelectModule,
  MatToolbarModule
} from '@angular/material';
import {NgMathPipesModule} from 'angular-pipes';
import { CommandInterfaceComponent } from './components/command-interface/command-interface.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MulticastCommandComponent } from './components/multicast-command/multicast-command.component';

@NgModule({
  declarations: [
    AppComponent,
    MachineListComponent,
    CommandInterfaceComponent,
    MulticastCommandComponent
  ],
  imports: [
    NgMathPipesModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    MatButtonModule,
    MatCardModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatInputModule,
    MatSelectModule,
    MatToolbarModule,
    ReactiveFormsModule
  ],
  entryComponents: [MulticastCommandComponent],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
