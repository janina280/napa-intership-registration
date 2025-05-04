import { Component, OnInit } from '@angular/core';
import {ShipService} from '../../services/ship.service';
import {PortService} from '../../services/port.service';
import {VoyageService} from '../../services/voyage.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
  shipLabels: string[] = [];
  shipData: number[] = [];

  portLabels: string[] = [];
  portData: number[] = [];

  voyageLabels: string[] = [];
  voyageData: number[] = [];

  constructor(
    private shipService: ShipService,
    private portService: PortService,
    private voyageService: VoyageService
  ) {}

  ngOnInit(): void {
    this.shipService.getAll().subscribe(ships => {
      this.shipLabels = ships.map(s => s.name);
      this.shipData = ships.map(s => s.maximumSpeed);
    });

    this.portService.getAll().subscribe(ports => {
      const countries = ports.map(p => p.country);
      this.portLabels = [...new Set(countries)];
      this.portData = this.portLabels.map(country =>
        ports.filter(p => p.country === country).length
      );
    });

    this.voyageService.getAll().subscribe(voyages => {
      const dates = voyages.map(v => v.voyageDate?.split('T')[0]);
      this.voyageLabels = [...new Set(dates)];
      this.voyageData = this.voyageLabels.map(date =>
        voyages.filter(v => v.voyageDate?.startsWith(date)).length
      );
    });
  }

  // Chart configs
  shipChartOptions = {
    responsive: true
  };

  portChartOptions = {
    responsive: true
  };

  voyageChartOptions = {
    responsive: true
  };
}
