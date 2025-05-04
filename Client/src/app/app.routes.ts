import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {VoyagesComponent} from './pages/voyages/voyages.component';
import {PortsComponent} from './pages/ports/ports.component';
import {ShipsComponent} from './pages/ships/ships.component';
import {DashboardComponent} from './pages/dashboard/dashboard.component';

export const routes: Routes = [
  { path: 'ships', component: ShipsComponent },
  { path: 'ports', component: PortsComponent },
  { path: 'voyages', component: VoyagesComponent },
  { path: '', component: DashboardComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
